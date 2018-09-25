using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Media;

namespace TestApplication {
  public class CitiesLoader {
    public IEnumerable<City> Load() {
      // https://raw.githubusercontent.com/d3/d3-plugins/master/graph/data/cities.csv
      using (FileStream fs = new FileStream(".\\cities.csv", FileMode.Open, FileAccess.Read)) {
        using (StreamReader sr = new StreamReader(fs)) {
          //skip first line
          sr.ReadLine();
          string line;

          while ((line = sr.ReadLine()) != null) {
            string[] values = line.Split(',');
            if (values.Length != 5) {
              continue;
            }
            yield return new City() {
              Name = values[0],
              Latitude = float.Parse(values[1], CultureInfo.InvariantCulture),
              Longitude = float.Parse(values[2], CultureInfo.InvariantCulture),
              Population = float.Parse(values[3], CultureInfo.InvariantCulture),
              Color = GetColor(values[4]),
            };
          }
        }
      }
    }

    private Color GetColor(string s) {
      byte r = Convert.ToByte(s.Substring(1, 2), 16);
      byte g = Convert.ToByte(s.Substring(3, 2), 16);
      byte b = Convert.ToByte(s.Substring(5, 2), 16);

      return Color.FromRgb(r, g, b);
    }
  }
}
