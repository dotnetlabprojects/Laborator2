﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laborator2.ViewModels
{
    public class CommentsGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Importan { get; set; }
        public int expenseId { get; set; }
    }
}