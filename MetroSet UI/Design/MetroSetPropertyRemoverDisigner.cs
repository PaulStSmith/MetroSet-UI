/*
 * MetroSet UI - MetroSet UI Framework
 * 
 * The MIT License (MIT)
 * Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
 * Copyright (c) 2023 Paulo Santos, https://github.com/PaulStSmith
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.Design;

namespace MetroSet.UI.Design
{
    /// <summary>
    /// Extends the design mode behavior of a <see cref="System.Windows.Forms.Control"/>.
    /// </summary>
    internal abstract class MetroSetPropertyRemoverDisigner : ControlDesigner
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        protected MetroSetPropertyRemoverDisigner() { }

        /// <summary>
        /// Get the names of properties to remove.
        /// </summary>
        /// <param name="propertiesToRemove">An <see cref="IEnumerable{T}"/> containing the names of the properties to remove.</param>
        protected abstract IEnumerable<string> GetPropertiesToRemove();

        /// <inheritdoc/>
        protected override void PostFilterProperties(IDictionary properties)
        {
            foreach (var property in GetPropertiesToRemove())
                properties.Remove(property);
            base.PostFilterProperties(properties);
        }
    }
}
