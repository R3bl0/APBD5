﻿using System.Text.RegularExpressions;
using FluentValidation;
using Animals.DTOs;
namespace Animals.Validators
{
    public class AnimalCreateRequestValidator: AbstractValidator<CreateAnimalRequest>
    {
        public AnimalCreateRequestValidator()
        {
            RuleFor(s => s.Name).MaximumLength(200).NotNull();
            RuleFor(s => s.Category).MaximumLength(200).NotNull();
            RuleFor(s => s.Area).MaximumLength(200).NotNull();
        }
    }
}