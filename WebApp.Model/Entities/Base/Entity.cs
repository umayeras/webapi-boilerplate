﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Model.Entities.Base
{
    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? ModifiedDate { get; set; }

        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; }

        [ScaffoldColumn(false)]
        public int StatusId { get; set; }

        protected Entity()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
            StatusId = (int) StatusType.Draft;
        }
    }
}
