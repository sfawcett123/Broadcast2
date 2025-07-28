# Test Plugin

## Project File
To overcome an error with ```typeof(IBroadcast).IsAssignableFrom(type)``` returning false the following entry needs to be made to your projects file.

```
  <ItemGroup>
    <ProjectReference Include="..\PluginBase\PluginBase.csproj">
        <Private>false</Private>
	    <ExcludeAssets>runtime</ExcludeAssets>
    </ProjectReference>
  </ItemGroup>
  ```

