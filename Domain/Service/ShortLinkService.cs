using Domain.Entity;
using Domain.Infrastructure;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ShortLinkService: IShortLinkService
    {
        private readonly MongoContext _mongoContext;

        public ShortLinkService(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void InsertShortLink(ShortLinkModels shortLink)
        {
            _mongoContext.ShortLinks.InsertOne(shortLink);
        }
    }
}
