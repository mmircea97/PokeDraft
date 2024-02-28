﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PokeDraft.Data;
using PokeDraft.Exceptions.Type;
using PokeDraft.Helpers;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Services.TypesServices
{
    public class TypesService : ITypesService
    {
        private readonly ApplicationDBContext _context;
        private const string _elementName = "type";

        public TypesService(ApplicationDBContext context)
        {
            _context = context;
        }


        //Returns all types from the database
        public async Task<IEnumerable<Type>> GetTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }

        //Returns a given type from the database
        public async Task<Type?> GetTypeByIdAsync(string typeName)
        {
            return await _context.Types.FindAsync(typeName);
        }


        //Adds a new type to the database
        //Returns: true if successful, throws custom exception if the name already exists in the database
        public async Task<bool> CreateTypeAsync(Type type)
        {
            if (await NameExists(type.TypeName))
            {
                throw new TypeAlreadyExistsException(String.Format(FailureMessages.ElementAlreadyExists, _elementName, type.TypeName));
            }

            _context.Types.Add(type);
            await _context.SaveChangesAsync();
            return true;
        }

        //Deletes a type from the database
        //Returns: true if successful, throws exception if the type does not exist
        public async Task<bool> DeleteTypeAsync(Type type)
        {
            if (!await NameExists(type.TypeName))
            {
                throw new TypeDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }


        //"Updates" a type from the database
        //As the primary key of the types table consists on the actual name of the type, instead of using update, we add the new type and then delete the previous one
        public async Task<Type?> UpdateTypeAsync(Type type, string typeName)
        {
            if(!await NameExists(type.TypeName))
            {
                throw new TypeDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }

            _context.Types.Add(new Type { TypeName = typeName });
            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
            return type;
        }




        //Searches for a type in the database
        //Returns true if type already exists, false otherwise
        public async Task<bool> NameExists(string typeName)
        {
            return await _context.Types.AnyAsync(a=>a.TypeName==typeName);
        }
    }
}
