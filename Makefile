clean:
	@find . -type d -name bin | xargs rm -rf
	@find . -type d -name obj | xargs rm -rf
	@echo "Done"

restore:
	@dotnet restore
	@echo "Done"

build:
	@dotnet build
	@echo "Done"

run:
	@dotnet exec ./bin/Debug/netcoreapp1.0/ExpressionsSerialization.dll

rebuild: clean restore build
