using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroSet.UI.Actions
{
    internal abstract class MetroSetBaseActionList<T> : DesignerActionList
    {
        protected MetroSetBaseActionList(IComponent component) : base(component) { }
    }
}
