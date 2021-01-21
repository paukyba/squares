using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squares.Data.DAL
{
    public interface IDAL
    {


    }

    public class DAL : IDAL
    {
        private readonly DbContext dbContext;

        public DAL(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}
