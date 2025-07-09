global using MediatR;
global using Microsoft.Extensions.Logging;
global using System.Diagnostics;
global using FluentValidation;
global using Microsoft.EntityFrameworkCore.Query;
global using System.Linq.Expressions;
global using SmokeZeroDigitalSolution.Application.Common.Models;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;
global using SmokeZeroDigitalSolution.Domain.Enums;
global using SmokeZeroDigitalSolution.Application.Common.IPersistence;
global using SmokeZeroDigitalSolution.Domain.Entites;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;
global using Microsoft.Extensions.DependencyInjection;
global using SmokeZeroDigitalSolution.Application.Common.Behaviors;
global using System.Reflection;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;
global using SmokeZeroDigitalSolution.Application.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User;
global using Microsoft.AspNetCore.Http;

global using System.Globalization;
global using System.Text.Json.Serialization;
global using System.Text.Json;
global using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

global using SmokeZeroDigitalSolution.Application.Common.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
global using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

global using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands;

global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries;

global using SmokeZeroDigitalSolution.Application.Common.Converter;

global using Microsoft.AspNetCore.Identity;

global using SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs;


global using SmokeZeroDigitalSolution.Application.Features.CoachManager.Interfaces;
