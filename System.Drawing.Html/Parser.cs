using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Drawing.Html
{
    /// <summary>
    /// Collection of regular expressions used when parsing
    /// </summary>
    internal static class Parser
    {
        /// <summary>
        /// Extracts properties and values from a Css property block; e.g. property:value;
        /// </summary>
		public const string CssProperties = @";?[^;\s]*:[^\{\}:;]*(\}|;)?";
		public static Regex CssPropertiesRegex = new Regex(CssProperties, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Extracts CSS style comments; e.g. /* comment */
        /// </summary>
        public const string CssComments = @"/\*[^*/]*\*/";
		public static Regex CssCommentsRegex = new Regex(CssComments, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS at-rules; e.g. @media print { block1{} block2{} }
        /// </summary>
        public const string CssAtRules = @"@.*\{\s*(\s*" + CssBlocks + @"\s*)*\s*\}";
		public static Regex CssAtRulesRegex = new Regex(CssAtRules, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts the media types from a media at-rule; e.g. @media print, 3d, screen {
        /// </summary>
        public const string CssMediaTypes = @"@media[^\{\}]*\{";
		public static Regex CssMediaTypesRegex = new Regex(CssMediaTypes, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts defined blocks in CSS. 
        /// WARNING: Blocks will include blocks inside at-rules.
        /// </summary>
        public const string CssBlocks = @"[^\{\}]*\{[^\{\}]*\}";
		public static Regex CssBlocksRegex = new Regex(CssBlocks, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts a number; e.g.  5, 6, 7.5, 0.9
        /// </summary>
        public const string CssNumber = @"{[0-9]+|[0-9]*\.[0-9]+}";
		public static Regex CssNumberRegex = new Regex(CssNumber, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts css percentages from the string; e.g. 100% .5% 5.4%
        /// </summary>
        public const string CssPercentage = @"([0-9]+|[0-9]*\.[0-9]+)\%"; //TODO: Check if works fine
		public static Regex CssPercentageRegex = new Regex(CssPercentage, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS lengths; e.g. 9px 3pt .89em
        /// </summary>
        public const string CssLength = @"([0-9]+|[0-9]*\.[0-9]+)(em|ex|px|in|cm|mm|pt|pc)";
		public static Regex CssLengthRegex = new Regex(CssLength, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS colors; e.g. black white #fff #fe98cd rgb(5,5,5) rgb(45%, 0, 0)
        /// </summary>
        public const string CssColors = @"(#\S{6}|#\S{3}|rgb\(\s*[0-9]{1,3}\%?\s*\,\s*[0-9]{1,3}\%?\s*\,\s*[0-9]{1,3}\%?\s*\)|maroon|red|orange|yellow|olive|purple|fuchsia|white|lime|green|navy|blue|aqua|teal|black|silver|gray)";
		public static Regex CssColorsRegex = new Regex(CssColors, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts line-height values (normal, numbers, lengths, percentages)
        /// </summary>
        public const string CssLineHeight = "(normal|" + CssNumber + "|" + CssLength + "|" + CssPercentage + ")";
		public static Regex CssLineHeightRegex = new Regex(CssLineHeight, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS border styles; e.g. solid none dotted
        /// </summary>
        public const string CssBorderStyle = @"(none|hidden|dotted|dashed|solid|double|groove|ridge|inset|outset)";
		public static Regex CssBorderStyleRegex = new Regex(CssBorderStyle, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS border widthe; e.g. 1px thin 3em
        /// </summary>
        public const string CssBorderWidth = "(" + CssLength + "|thin|medium|thick)";
		public static Regex CssBorderWidthRegex = new Regex(CssBorderWidth, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts font-family values
        /// </summary>
        public const string CssFontFamily = "(\"[^\"]*\"|'[^']*'|\\S+\\s*)(\\s*\\,\\s*(\"[^\"]*\"|'[^']*'|\\S+))*";
		public static Regex CssFontFamilyRegex = new Regex(CssFontFamily, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS font-styles; e.g. normal italic oblique
        /// </summary>
        public const string CssFontStyle = "(normal|italic|oblique)";
		public static Regex CssFontStyleRegex = new Regex(CssFontStyle, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts CSS font-variant values; e.g. normal, small-caps
        /// </summary>
        public const string CssFontVariant = "(normal|small-caps)";
		public static Regex CssFontVariantRegex = new Regex(CssFontVariant, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts font-weight values; e.g. normal, bold, bolder...
        /// </summary>
        public const string CssFontWeight = "(normal|bold|bolder|lighter|100|200|300|400|500|600|700|800|900)";
		public static Regex CssFontWeightRegex = new Regex(CssFontWeight, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Exracts font sizes: xx-small, larger, small, 34pt, 30%, 2em
        /// </summary>
        public const string CssFontSize = "(" + CssLength + "|" + CssPercentage + "|xx-small|x-small|small|medium|large|x-large|xx-large|larger|smaller)";
		public static Regex CssFontSizeRegex = new Regex(CssFontSize, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Gets the font-size[/line-height]? on the font shorthand property.
        /// Check http://www.w3.org/TR/CSS21/fonts.html#font-shorthand
        /// </summary>
        public const string CssFontSizeAndLineHeight = CssFontSize + @"(\/" + CssLineHeight + @")?(\s|$)";
		public static Regex CssFontSizeAndLineHeightRegex = new Regex(CssFontSizeAndLineHeight, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts HTML tags
        /// </summary>
        public const string HtmlTag = @"<[^<>]*>";
		public static Regex HtmlTagRegex = new Regex(HtmlTag, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        /// <summary>
        /// Extracts attributes from a HTML tag; e.g. att=value, att="value"
        /// </summary>
        public const string HmlTagAttributes = "[^\\s]*\\s*=\\s*(\"[^\"]*\"|[^\\s]*)";
		public static Regex HmlTagAttributesRegex = new Regex(HmlTagAttributes, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled); 

        #region Methods

		public static MatchCollection Match(Regex regex, string source)
        {
            return regex.Matches(source);
        }

		public static string Search(Regex regex, string source) {
			int position;
			return Search(regex, source, out position);
		}

		public static string Search(Regex regex, string source, out int position) {
			MatchCollection matches = Match(regex, source);

			if (matches.Count > 0) {
				position = matches[0].Index;
				return matches[0].Value;
			}
			else {
				position = -1;
			}

			return null;
		}

        #endregion
    }
}
