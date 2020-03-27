FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PedidoAPI/PedidoAPI.csproj", "PedidoAPI/"]
RUN dotnet restore "PedidoAPI/PedidoAPI.csproj"
COPY . .
WORKDIR "/src/PedidoAPI"
RUN dotnet build "PedidoAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PedidoAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PedidoAPI.dll"]