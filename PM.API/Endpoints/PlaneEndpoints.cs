using AutoMapper;


using Microsoft.AspNetCore.Mvc;
using PM.Buisness.Repositories.IRepositories;
using PM.Data.Entity;
using PM.Models;
using System.Net;

namespace PM.API.Endpoints
{
    public static class PlaneEndpoints
    {
        public static void ConfigurePlaneEndpoints(this WebApplication app)
        {
            app.MapGet("/api/plane", GetAllPlane)
                .WithName("GetPlanes")
                .Produces<APIResponse>(200);
     

            app.MapGet("/api/plane/{id:int}", GetPlane)
                .WithName("GetPlane")
                .Produces<APIResponse>(200);

            app.MapPost("/api/plane", CreatePlane)
                .WithName("CreatePlane")
                .Accepts<PlaneCreateDTO>("application/json")
                .Produces<APIResponse>(201)
                .Produces(400);

            app.MapPut("/api/plane", UpdatePlane)
                .WithName("UpdatePlane")
                .Accepts<PlaneUpdateDTO>("application/json")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapDelete("/api/plane/{id:int}", DeletePlane);
        }

        private async static Task<IResult> DeletePlane(IPlaneRepositorie planeRepository, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            PlaneDTO planeFromStore = await planeRepository.GetAsync(id);
            if (planeFromStore != null)
            {
                await planeRepository.RemoveAsync(planeFromStore.Id);
                //await planeRepository.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> UpdatePlane(
            IPlaneRepositorie planeRepository,
            IMapper mapper,
            [FromBody] PlaneUpdateDTO plane_U_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            await planeRepository.UpdateAsync(mapper.Map<PlaneDTO>(plane_U_DTO));
            //await planeRepository.SaveAsync();

            response.Result = mapper.Map<PlaneDTO>(await planeRepository.GetAsync(plane_U_DTO.Id)); ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreatePlane(
            IPlaneRepositorie planeRepository,
            IMapper mapper,
            [FromBody] PlaneCreateDTO plane_C_DTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (planeRepository.GetAsync(plane_C_DTO.Name).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Plane name already exists");
                return Results.BadRequest(response);
            }

            PlaneDTO plane = mapper.Map<PlaneDTO>(plane_C_DTO);

            await planeRepository.CreateAsync(plane);
            //await planeRepository.SaveAsync();

            PlaneDTO couponDTO = mapper.Map<PlaneDTO>(plane);
            response.Result = couponDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);

   
        }

       
        private async static Task<IResult> GetAllPlane(
            IPlaneRepositorie _planeRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all planes");
            response.Result = await _planeRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetPlane(
            IPlaneRepositorie _planeRepo, ILogger<Program> _logger, int id)
        {
            APIResponse response = new();
            response.Result = await _planeRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

    }
}
