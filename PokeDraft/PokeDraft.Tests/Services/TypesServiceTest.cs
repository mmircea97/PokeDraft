using PokeDraft.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeDraft.Services.TypesServices;
using Type = PokeDraft.Models.Type;
using FluentAssertions;
using PokeDraft.Exceptions;
using PokeDraft.Exceptions.Type;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace PokeDraft.Tests.Controller
{
    public class TypesServiceTest
    {
        private readonly ApplicationDBContext _contextInMemory;
        private readonly TypesService _typesService;

        public TypesServiceTest()
        {
            _contextInMemory = Helpers.DBContextHelper.GetDBContext();
            _typesService = new TypesService(_contextInMemory);
        }

        [Fact]
        public async Task DeleteType_TypeDoesNotExist_ReturnFalseAsync()
        {
            Func<Task> func = async () => await _typesService.DeleteTypeAsync(new Type { TypeName = "Mud"});
            Func<Task> func2 = async () => await _typesService.DeleteTypeAsync(new Type { TypeName = "Water" });

            await func.Should().ThrowAsync<TypeDoesNotExistException>();
            func2.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTypes_TypesAreNotNull_ReturnFalseAsync()
        {
            var result = await _typesService.GetTypesAsync();

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetTypeById_TypeExists_ReturnType()
        {
            var result = await _typesService.GetTypeByIdAsync("Steel");
            result.Should().NotBeNull();
        }


    }
}
