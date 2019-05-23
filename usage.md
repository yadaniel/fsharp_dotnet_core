# create named solution file in current directory  
# create console projects types and functions in own directories  
# add projects to solution, build projects and run projects  
dotnet new sln -n tutorial  
dotnet new console -lang=F# -n types  
dotnet new console -lang=F# -n functions  
dotnet sln tutorial.sln add types  
dotnet sln tutorial.sln add functions  
dotnet build types  
dotnet build functions  
cd types && dotnet run bin/Debug/netcoreapp2.1/types.pdb && cd ..  
cd functions/ && dotnet run bin/Debug/netcoreapp2.1/functions.pdb && cd ..  


# create solution file in named directory  
dotnet new sln -o tutorial  
cd tutorial  
dotnet new console -lang=F# -n types  
dotnet new console -lang=F# -n functions  
dotnet sln tutorial.sln add types functions/  
dotnet build types  
dotnet build functions  
cd types && dotnet run bin/Debug/netcoreapp2.1/types.pdb && cd ..  
cd functions/ && dotnet run bin/Debug/netcoreapp2.1/functions.pdb && cd ..  

