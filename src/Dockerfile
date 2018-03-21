FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
EXPOSE 80
COPY . /app
ENV ASPNETCORE_ENVIRONMENT="testing"
ENTRYPOINT ["dotnet", "Push.dll"]
