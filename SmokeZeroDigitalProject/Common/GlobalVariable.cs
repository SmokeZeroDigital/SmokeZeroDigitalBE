global using SmokeZeroDigitalProject.Common.Handlers;
global using SmokeZeroDigitalProject.Common.Middlewares;
global using SmokeZeroDigitalProject.Common.Swagger;
global using SmokeZeroDigitalSolution.Application;
global using SmokeZeroDigitalSolution.Infrastructure;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using SmokeZeroDigitalProject.Common.Base;
global using SmokeZeroDigitalProject.Common.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.Queries;
global using SmokeZeroDigitalSolution.Contracts.Auth;
global using Microsoft.AspNetCore.Diagnostics;
global using SmokeZeroDigitalProject.Common.Models;
global using Error = SmokeZeroDigitalProject.Common.Models.Error;
global using SmokeZeroDigitalSolution.Application.Common.Models;
global using SmokeZeroDigitalProject.Common.Services;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Queries;
global using SmokeZeroDigitalSolution.Contracts.Plan;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User;
global using SmokeZeroDigitalSolution.Contracts.User;
global using Microsoft.AspNetCore.SignalR;
global using SmokeZeroDigitalSolution.Application.Common.Interfaces;
global using SmokeZeroDigitalSolution.Application.Features.Chat.DTOs;

global using SmokeZeroDigitalProject.Common.Realtime;
global using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;
global using SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;

global using System.Globalization;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using System.Reflection;

global using Microsoft.OpenApi.Any;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.SwaggerGen;

global using SmokeZeroDigitalSolution.Application.Features.Chat.Commands;
global using SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetMessages;
global using SmokeZeroDigitalSolution.Application.Features.Chat.Queries.GetOrCreateConversation;

global using SmokeZeroDigitalSolution.Application.Features.CommentManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.CommentManager.Queries;
global using SmokeZeroDigitalSolution.Contracts.Comment;

global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Commands;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Queries;
global using SmokeZeroDigitalSolution.Contracts.Feedback;

global using Microsoft.AspNetCore.Localization;
global using SmokeZeroDigitalProject.Common.Converter;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Chat;

global using Microsoft.AspNetCore.Mvc.RazorPages;

global using SmokeZeroDigitalProject.Helpers;
