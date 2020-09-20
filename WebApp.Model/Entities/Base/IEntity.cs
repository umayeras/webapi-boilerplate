﻿using System;

namespace WebApp.Model.Entities.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
        int StatusId { get; set; }
    }
}
