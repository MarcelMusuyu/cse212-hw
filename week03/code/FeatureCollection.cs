// --- Challenge 5: Earthquake JSON Data - Supporting Classes ---
// Define classes to map the JSON structure from USGS
// These are defined outside the SetsAndMaps class for clarity as data models
public class FeatureCollection
{
    public string Type { get; set; }
    public Metadata Metadata { get; set; }
    public Feature[] Features { get; set; }
    public double[] Bbox { get; set; }
}

public class Metadata
{
    public long Generated { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string Api { get; set; }
    public int Count { get; set; }
}

public class Feature
{
    public string Type { get; set; }
    public Properties Properties { get; set; }
    public Geometry Geometry { get; set; }
    public string Id { get; set; }
}

public class Properties
{
    public double Mag { get; set; }
    public string Place { get; set; }
    public long Time { get; set; }
    public long Updated { get; set; }
    public int? Tz { get; set; } // Nullable in JSON
    public string Url { get; set; }
    public string Detail { get; set; }
    public int? Felt { get; set; } // Nullable in JSON
    public double? Cdi { get; set; } // Nullable in JSON
    public double? Mmi { get; set; } // Nullable in JSON
    public string Alert { get; set; } // Nullable
    public string Status { get; set; }
    public int Tsunami { get; set; }
    public int Sig { get; set; }
    public string Net { get; set; }
    public string Code { get; set; }
    public string Ids { get; set; }
    public string Sources { get; set; }
    public string Types { get; set; } // Note: This property name 'Types' conflicts with a property named 'Type' in Feature. This is a common issue with JSON mapping if names aren't unique. Assuming 'Type' from JSON maps to this 'Types' in C# based on typical USGS structure.
    public int? Nst { get; set; } // Nullable
    public double? Dmin { get; set; } // Nullable
    public double Rms { get; set; }
    public int Gap { get; set; }
    public string MagType { get; set; }
    public string Type { get; set; } // This is also 'type' in JSON, but maps to Feature.Type. Properties also has a 'type' field.
    public string Title { get; set; }
}

public class Geometry
{
    public string Type { get; set; }
    public double[] Coordinates { get; set; }
}
