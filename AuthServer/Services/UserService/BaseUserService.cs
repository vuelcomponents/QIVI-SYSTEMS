﻿using authServer.Options;
using authServer.Repositories.UserRepository;
using authServer.Services.MailerService;
using authServer.Services.TokenService_;
using AutoMapper;
using ClassLibrary.Utils;
using Microsoft.Extensions.Options;

namespace authServer.Services.UserService;

public class BaseUserService(
    IMapper mapper,
    IQuickActions quickActions,
    IMailerVerificationService mailerVerificationService,
    IUserRepository userRepository,
    ITokenWriteService tokenWriteService,
    IOptions<JwtOptions> jwtOptions
)
{
    protected readonly IMapper Mapper = mapper;
    protected readonly IQuickActions QuickActions = quickActions;
    protected readonly IMailerVerificationService MailerVerificationService =
        mailerVerificationService;
    protected readonly IUserRepository UserRepository = userRepository;
    protected readonly ITokenWriteService TokenWriteService = tokenWriteService;
    protected readonly IOptions<JwtOptions> JwtOptions = jwtOptions;
}
