﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace SolidValidation.ViewModels {
    public class ObservableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged<TViewModel>(Expression<Func<TViewModel>> property) {
            var expression = property.Body as MemberExpression;
            var member = expression.Member;

            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(member.Name));
            }
        }
    }

}
