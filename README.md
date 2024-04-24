# Harmonix - Seamlessly Generate Unique Spotify Playlists
Harmonix utilizes AI to generate playlists based on thematic prompts or existing tracks/artists.

## How to Use:
I have ignored the appsettings.json file for security purposes, so you will need to create your own in order to link your OpenAI API key

 1. Create a file called `appsettings.json` in the root directory
 2. Within your `appsettings.json` file copy and paste the following:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "OpenAI": {
    "APIKey": "your-api-key-here"
  },
  "Spotify": {
    "ClientID": "18ff1811ccc84432bbe7dca38e272c50"
  },
  "AllowedHosts": "*"
}
```
 3. Create OpenAI API key at https://platform.openai.com/apps
 4. Replace "your-api-key-here" with your API key
 5. Congrats! You can now use Harmonix