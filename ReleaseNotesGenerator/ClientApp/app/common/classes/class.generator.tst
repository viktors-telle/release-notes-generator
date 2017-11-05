${
    using Typewriter.Extensions.Types;

    Template(Settings settings)
    {
        settings.OutputExtension = ".ts";        
    }

    string Inherit(Class c)
    {
        if (c.BaseClass == null)
        {
            return  "";
        }
        if (c.BaseClass.IsGeneric)
        {
            return " extends " + c.BaseClass.ToString() + $"<{c.BaseClass.TypeArguments.First()}>";
        }
	    return " extends " + c.BaseClass.ToString();
    }

    string Imports(Class c) 
    {   
        var neededImports = c.Properties
	        .Where(p => !p.Type.IsPrimitive || p.Type.IsEnum)
            .Where(p => !p.Type.Name.Equals("T"))
	        .Select(p => "import { " + p.Type.Name.TrimEnd('[',']') + " } from './" + p.Type.Name.TrimEnd('[',']') + "';").ToList();
        
        if (c.BaseClass != null) 
        { 
	        neededImports.Add("import { " + c.BaseClass.Name +" } from './" + c.BaseClass.Name + "';");
        }
        return string.Join("\n", neededImports.Distinct());
    }
}
    $Classes(ReleaseNotesGenerator.Domain.*)[$Imports
    export class $Name$TypeParameters$Inherit {
        $Properties[
        $name: $Type;]
        }]

    $Enums(ReleaseNotesGenerator.Enums.*)[
            export enum $Name {$Values[
            $name = $Value,]
    }]