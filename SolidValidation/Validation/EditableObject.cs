using System.Collections.Generic;
using System.Linq;
using System;
using SolidValidation.ViewModels;

namespace SolidValidation.Validation {
    public class EditableObject : ObservableObject {
        public bool HasErrors {
            get {
                return CountErrors > 0;
            }
        }

        public int CountErrors {
            get {
                var editProps = GetRecursiveEditProps(this);
                var count = editProps.Sum(x => x.ValidationErrors.Count);
                return count;
            }
        }

        /// <summary>
        /// This is an automated copy function to copy all
        /// values from the original object into the EditableObject
        /// It uses reflection to find all the properties
        /// </summary>
        public void CopyFromSourceToUI(object sourceData) {
            // get all properties from source object
            var sourceProps = sourceData.GetType().GetProperties();
            foreach (var prop in sourceProps) {
                // get value from source object property
                var sourceValue = prop.GetValue(sourceData, null);
                var destProp = this.GetType().GetProperty(prop.Name);
                var editProp = destProp.GetValue(this, null) as IEditProp;
                if (editProp != null) {
                    editProp.Text = sourceValue + "";
                } else {
                    if (sourceValue != null) {
                        destProp.SetValue(this, sourceValue, null);
                    }
                }
            }
        }

        /// <summary>
        /// This is an automated copy function to copy all
        /// values from the EditableObject to the original object
        /// It uses reflection to find all the properties
        /// </summary>
        public void CopyFromUIToSource(object sourceData) {
            var sourceProps = sourceData.GetType().GetProperties();
            foreach (var prop in sourceProps) {
                var destProp = this.GetType().GetProperty(prop.Name);
                var editProp = destProp.GetValue(this, null) as IEditProp;
                if (editProp != null) {
                    prop.SetValue(sourceData, editProp.GetValue(), null);
                } else {
                    var v = destProp.GetValue(this, null);
                    if (v != null) {
                        prop.SetValue(sourceData, v, null);
                    }
                }
            }
        }
        public List<ValidationError> ValidationErrors { get; private set; }

        public List<IEditProp> GetRecursiveEditProps(EditableObject o) {
            var list = new List<IEditProp>();
            var props = o.GetType().GetProperties();
            foreach (var prop in props.Where(x => x.Name != "CountErrors" && x.Name != "HasErrors")) {
                var editProp = prop.GetValue(o, null) as IEditProp;
                if (editProp != null)
                {
                    list.Add(editProp);
                }

                // kijk of het een object met propties is
                var editObj = prop.GetValue(o, null) as EditableObject;
                if (editObj != null)
                {
                    list.AddRange(GetRecursiveEditProps(editObj));
                }
            }
            return list;
        }

        public virtual void Validate(Action ready) {
            ValidationErrors = new List<ValidationError>();
            var editProps = GetRecursiveEditProps(this);
            int editPropCounter = editProps.Count;
            foreach (var editProp in editProps) {
                editProp.Validate(ep => {
                    if (ep.HasError) {
                        ValidationErrors.Add(ep.ValidationErrors.First());
                    }
                    editPropCounter--;
                    if (editPropCounter == 0) {
                        ready();
                    }
                });
            }
        }

        // not used yet. this is an example
        public void AddCustomError(string message) {
            ValidationErrors.Add(new ValidationError {
                Validator = new CustomValidator { ValidationFailedMessage = message }
            });
        }
    }
}
