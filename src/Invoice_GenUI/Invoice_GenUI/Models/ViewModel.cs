﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Invoice_GenUI.Models
{
    public abstract class ViewModel : ObservableValidator, IDataErrorInfo
    {
        public string Error { get { return null; } }
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(GetType().GetProperty(columnName).GetValue(this), new ValidationContext(this)
                {
                    MemberName = columnName
                },
                validationResults)) return null;

                return validationResults.First().ErrorMessage;
            }
        }
    }
}
