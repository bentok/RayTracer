SOLUTION = RayTracer.sln

project ?= RayTracer
action ?= add

.PHONY: build clean restore test run publish help

install:
	dotnet paket install

restore:
	dotnet paket restore

build: restore
	dotnet build

tests:
	 dotnet test RayTracerTests/RayTracerTests.fsproj --logger:"console;verbosity=detailed"

fantomas:
	dotnet tool restore
	dotnet fantomas ./ --recurse

clean:
	dotnet clean

run:
	dotnet run --project RayTracer/RayTracer.fsproj

add_or_remove:
	dotnet paket $(action) --project RayTracer/RayTracer.fsproj $(package)

add_or_remove_tests:
	dotnet paket $(action) --project RayTracerTests/RayTracerTests.fsproj $(package)
