global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Query;
global using System.Linq.Expressions;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using SmokeZeroDigitalSolution.Domain.Entites;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.Interfaces;
global using SmokeZeroDigitalSolution.Infrastructure.Persistence.Data;
global using SmokeZeroDigitalSolution.Infrastructure.Persistence.Services;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Http;
global using Microsoft.IdentityModel.Tokens;
global using SmokeZeroDigitalSolution.Application.Interfaces;
global using System.Text;
global using System.Text.Json;
global using Microsoft.Extensions.Options;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using SmokeZeroDigitalSolution.Application.Common.IPersistence;
global using SmokeZeroDigitalSolution.Infrastructure.Persistence.Common;
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.Auth;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Identity;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.JWT;

global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.Plan;
global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.Interface;

global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Payment.VNPAY;
global using SmokeZeroDigitalSolution.Infrastructure.Persistence.Repositories;
 
global using SmokeZeroDigitalSolution.Application.Features.UsersManager.DTOs.User;
global using UAParser;

global using SmokeZeroDigitalSolution.Application.Features.SubScriptionPlanManager.DTOs.VNPay;

global using SmokeZeroDigitalSolution.Application.Features.Chat.Interfaces;

global using SmokeZeroDigitalSolution.Application.Features.CommentManager.Interfaces;

global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Google.Auth;

global using Google.Apis.Auth;

global using System.Security.Cryptography;


global using System.Globalization;
global using System.Net.Sockets;
global using System.Net;

global using SmokeZeroDigitalSolution.Application.Features.CommentManager.DTOs;

global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.FeedbackManager.Interfaces;

global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Comment;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Chat;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Payment;
global using SmokeZeroDigitalSolution.Infrastructure.ExternalServices.Google;

global using SmokeZeroDigitalSolution.Application.Features.CoachManager.DTOs;
global using SmokeZeroDigitalSolution.Application.Features.CoachManager.Interfaces;
