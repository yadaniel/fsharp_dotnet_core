dotnet new sln -n tutorial
dotnet new console -lang=F# -n types
dotnet new console -lang=F# -n functions
dotnet build types
dotnet build functions
cd types && dotnet run bin/Debug/netcoreapp2.1/types.pdb && cd ..
cd functions/ && dotnet run bin/Debug/netcoreapp2.1/functions.pdb && cd ..
