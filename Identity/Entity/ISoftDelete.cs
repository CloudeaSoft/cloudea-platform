﻿namespace Identity.Entity;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}