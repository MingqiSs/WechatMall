﻿using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace X.Models.WMDB
{
    [Serializable]
    public partial class Sys_Roleauth
    {
           public Sys_Roleauth(){}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? CreateDate { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Creator { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public int Menu_Id { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Modifier { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? ModifyDate { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public int? Role_Id { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [Key]
           public int Auth_Id { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string UID { get; set; }

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           public string AuthValue { get; set; }


    }
}
