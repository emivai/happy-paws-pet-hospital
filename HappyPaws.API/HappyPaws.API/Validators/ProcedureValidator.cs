﻿using FluentValidation;
using HappyPaws.API.Contracts.DTOs.ProcedureDTOs;
using HappyPaws.Core.Entities;

namespace HappyPaws.API.Validators
{
    public class CreateProcedureValidator : AbstractValidator<CreateProcedureDTO>
    {
        public CreateProcedureValidator()
        {
            RuleFor(procedure => procedure.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(procedure => procedure.Name).Length(1, 100).WithMessage("Name has to be 1-100 characters long.");

            RuleFor(procedure => procedure.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(procedure => procedure.Description).Length(1, 250).WithMessage("Name has to be 1-250 characters long.");

            RuleFor(procedure => procedure.Price).NotEmpty().WithMessage("Price is required.");
            RuleFor(procedure => procedure.Price).InclusiveBetween(0, 10000).WithMessage("Price has to be in range 0 - 10000.");
        }      
    }

    public class UpdateProcedureValidator : AbstractValidator<UpdateProcedureDTO>
    {
        public UpdateProcedureValidator()
        {
            RuleFor(procedure => procedure.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(procedure => procedure.Name).Length(1, 100).WithMessage("Name has to be 1-100 characters long.");

            RuleFor(procedure => procedure.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(procedure => procedure.Description).Length(1, 250).WithMessage("Name has to be 1-250 characters long.");

            RuleFor(procedure => procedure.Price).NotEmpty().WithMessage("Price is required.");
            RuleFor(procedure => procedure.Price).InclusiveBetween(0, 10000).WithMessage("Price has to be in range 0 - 10000.");
        }
    }
}
