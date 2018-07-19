﻿using App.Common.Interface;
using FluentValidation;
using FluentValidation.Results;

namespace App.Common.Validator
{
    public interface IMessageValidator<T> where T : IMessage
    {
        ValidationResult Validate(ValidationContext<T> context);
    }

    public abstract class MessageValidator<T> :
        AbstractValidator<T>,
        IMessageValidator<T> where T : IMessage
    {
    }
}