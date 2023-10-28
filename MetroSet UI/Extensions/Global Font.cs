using System.Drawing;

namespace MetroSet.UI.Extensions
{
    public static class GlobalFont
    {

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Regular(float size) => new("Segoe UI", size);

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="fnt">The Font name.</param>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Normal(string fnt, float size) => new(fnt, size);

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Light(float size) => new("Segoe UI Light", size);

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Italic(float size) => new("Segoe UI", size, FontStyle.Italic);

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font SemiBold(float size) => new("Segoe UI semibold", size);

        /// <summary>
        /// Gets the font for the most controls.
        /// </summary>
        /// <param name="size">The Size of the Segoe UI font.</param>
        /// <returns>The Segoe UI font with the given size.</returns>
        public static Font Bold(float size) => new("Segoe UI", size, FontStyle.Bold);

    }
}