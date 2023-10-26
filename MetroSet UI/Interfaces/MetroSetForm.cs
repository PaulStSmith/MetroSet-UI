using MetroSet.UI.Components;
using MetroSet.UI.Enums;

namespace MetroSet.UI.Interfaces
{
	public interface iMetroSetForm
	{
		/// <summary>
		///
		/// </summary>
		Style Style { get; set; }

		/// <summary>
		///
		/// </summary>
		StyleManager StyleManager { get; set; }

		string ThemeAuthor { get; set; }

		string ThemeName { get; set; }

	}
}