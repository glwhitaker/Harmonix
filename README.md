# Harmonix - Seamlessly Generate Unique Spotify Playlists
Harmonix utilizes AI to generate playlists based on thematic prompts or existing tracks/artists.

## How to Use:
### Step 1
I have ignored the appsettings.json file for security purposes, so you will need to create your own in order to link you API Keys

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
    "APIKey": "your-api-key-here"
  },
  "AllowedHosts": "*"
}
```
 3. Create OpenAI and Spotify API keys
 4. Replace "your-api-key-here" with your API keys
 5. Congrats! You can now use Harmonix
