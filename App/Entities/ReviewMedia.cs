﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RekomBackend.App.Entities;

public class ReviewMedia : EntityBase
{
   #region Columns

   public string MediaUrl { get; set; } = null!;
   
   public string Type { get; set; } = null!;

   #endregion
   
   #region ForeignKeys

   public string ReviewId { get; set; } = null!;

   #endregion

   #region Relationships

   public Review? Review { get; set; }
   
   #endregion

   #region Methods

   #endregion
}