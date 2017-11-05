${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.OutputExtension = ".ts";        
    }

    string Inherit(Class c)
    {
        if (c.BaseClass != null)
        {
            if (c.BaseClass.IsGeneric)
            {
                return " extends " + c.BaseClass.ToString() + $"<{c.BaseClass.TypeArguments.First()}>";
            }
	        return " extends " + c.BaseClass.ToString();
        }
  
	    return  "";
    }

    string Imports(Class c) 
    {   
        List<string> neededImports = c.Properties
	        .Where(p => !p.Type.IsPrimitive)
            .Where(p => !p.Type.Name.Equals("T"))
	        .Select(p => "import { " + p.Type.Name.TrimEnd('[',']') + " } from './" + p.Type.Name.TrimEnd('[',']') + "';").ToList();
        neededImports.AddRange(c.Properties
	        .Where(p => p.Type.IsEnum)
	        .Select(p => "import { " + p.Type.Name.TrimEnd('[',']') + " } from './" + p.Type.Name.TrimEnd('[',']') + "';").ToList());
        if (c.BaseClass != null) 
        { 
	        neededImports.Add("import { " + c.BaseClass.Name +" } from './" + c.BaseClass.Name + "';");
        }
        return String.Join("\n", neededImports.Distinct());
    }
}
    $Classes(ReleaseNotesGenerator.Domain.*)[$Imports
    export class $Name$TypeParameters$Inherit {
        $Properties[
        $name: $Type]
        }]

    $Enums(ReleaseNotesGenerator.Enums.*)[
            export enum $Name {$Values[
            $name = $Value,]
    }]