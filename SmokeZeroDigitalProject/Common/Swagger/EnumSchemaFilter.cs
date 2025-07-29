namespace SmokeZeroDigitalProject.Common.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                foreach (var name in Enum.GetNames(context.Type))
                {
                    schema.Enum.Add(new OpenApiString(name));
                }

                schema.Description += $"<br/>Possible values: {string.Join(", ", Enum.GetNames(context.Type))}";
            }
        }
    }
}