using AutoMapper;
using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageMangamentService _imageMangamentService;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _connectionMultiplexer_redis;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService; 
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGenrateToken Token;
        private readonly IConfiguration _configuration;
        public UnitOfWork(ApplicationDbContext context,
            IMapper mapper,
            IImageMangamentService imageMangamentService,
            IConnectionMultiplexer connectionMultiplexer_redis,
            UserManager<AppUser> userManager,
            IEmailService emailService,
            SignInManager<AppUser> signInManager,
            IGenrateToken token,
            IConfiguration configuration
            )
        {
            _context = context;
            _mapper = mapper;
            this._userManager = userManager;
            this._emailService = emailService;
            this._signInManager = signInManager;
            this.Token = token;
            this._configuration = configuration;

            this._connectionMultiplexer_redis = connectionMultiplexer_redis;

            _imageMangamentService = imageMangamentService;
            ProductRepository = new ProductRepositry(_context, _mapper, _imageMangamentService);

            CategoryRepository = new CategoryRepositry(_context,mapper);

            PhotoRepository = new PhotoRepoistory(_context);

            RatingRepository = new RatingRepositry(_context);

            CustomerBasketRepository = new CustomerBasketRepository(connectionMultiplexer_redis);
            Auth = new AuthRepository(_userManager, _emailService, _signInManager, Token, _configuration);
        }

        public IProductRepoistry ProductRepository { get; }

        public ICategoryRepositry CategoryRepository { get; }
        public IPhotoRepositry PhotoRepository { get; }

        public IRatingRepositry RatingRepository { get; }

        public ICustomerBasketRepository CustomerBasketRepository { get; }

        public IAuth Auth { get; }

    }
}
