namespace TexturePackerLoader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
#if NETFX_CORE
    using System.Threading.Tasks;
#endif
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class SpriteSheetLoader
    {
        private readonly ContentManager contentManager;

#if __IOS__
        private readonly bool supportRetina;
#endif

        public SpriteSheetLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;

#if __IOS__
            this.supportRetina = MonoTouch.UIKit.UIScreen.MainScreen.Scale == 2.0f;
#endif
        }

        public SpriteSheet MultiLoad(string imageResourceFormat, int numSheets)
        {
            SpriteSheet result = new SpriteSheet();
            for (int i = 0; i < numSheets; i++)
            {
                string imageResource = string.Format(imageResourceFormat, i);

                SpriteSheet tmp = Load(imageResource);
                result.Add(tmp);
            }
            return result;
        }


        public SpriteSheet Load(string imageResource)
        {
            var texture = this.contentManager.Load<Texture2D>(imageResource);

            var dataFile = Path.Combine(
                this.contentManager.RootDirectory,
                Path.ChangeExtension(imageResource, "txt"));

            var dataFileLines = this.ReadDataFile(dataFile);

            var sheet = new SpriteSheet();

            foreach (
                var cols in
                    from row in dataFileLines
                    where !string.IsNullOrEmpty(row) && !row.StartsWith("#")
                    select row.Split(';'))
            {
                if (cols.Length != 10)
                {
                    throw new InvalidDataException("Incorrect format data in spritesheet data file");
                }

                var isRotated = int.Parse (cols [1]) == 1;
                var name = cols[0];
                var sourceRectangle = new Rectangle(
                    int.Parse(cols[2]),
                    int.Parse(cols[3]),
                    int.Parse(cols[4]),
                    int.Parse(cols[5]));
                var size = new Vector2(
                    int.Parse(cols[6]),
                    int.Parse(cols[7]));
                var pivotPoint = new Vector2(
                    float.Parse(cols[8]),
                    float.Parse(cols[9]));
                var sprite = new SpriteFrame(texture, sourceRectangle, size, pivotPoint, isRotated);

                sheet.Add(name, sprite);
            }

            return sheet;
        }

#if __IOS__
        private string[] ReadDataFile(string dataFile) 
        {
            var input = dataFile;

            if (this.supportRetina)
            {
                var dataFile2x = Path.Combine (Path.GetDirectoryName (dataFile),
                    Path.GetFileNameWithoutExtension (dataFile)
                    + "@2x" + Path.GetExtension (dataFile));

                if (File.Exists (dataFile2x))
                {
                    input = dataFile2x;
                }
            }

            return File.ReadAllLines (input);
        }
#elif NETFX_CORE
        private string[] ReadDataFile(string dataFile)
        {
            var dataFileLines = this.ReadDataFileLines(dataFile);

            return dataFileLines.Result.ToArray();
        }

        private async Task<string[]> ReadDataFileLines(string dataFile)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            var file = await folder.GetFileAsync(dataFile).AsTask().ConfigureAwait(false);
            var fileContents = await Windows.Storage.FileIO.ReadLinesAsync(file).AsTask().ConfigureAwait(false);

            return fileContents.ToArray();
        }
#elif __ANDROID__
		private string[] ReadDataFile(string dataFile) {
			using(var ms = new MemoryStream()) {
				using (var s = Game.Activity.Assets.Open (dataFile)) {
					s.CopyTo (ms);
					return System.Text.Encoding.Default.GetString (ms.ToArray()).Split (new char[] { '\n'});
				}
			}
		}
#else
        private string[] ReadDataFile(string dataFile) 
        {
            return File.ReadAllLines(dataFile);
        }
#endif
    }
}