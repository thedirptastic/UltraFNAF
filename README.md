## UltraFNAF
A simple mod that will launch FNAF (or really any game) on death. It is preset to FNAF 2, but you can edit the config file to point to any game exe. Please note that you have to use / (foward slashes) in the config file, as for some reason \ (backslashes) seem to break the mod. 

You can also use steam links, like `steam://rungameid/70` as executable paths. Just put the game link in the config.
## Building
Install dotnet version 6.0.420
```
git clone https://github.com/thedirptastic/UltraFNAF.git
cd UltraFNAF
dotnet build -p:ULTRAKILLPath="ULTRAKILL Path"
```

You must also have your Assembly-CSharp.dll in ULTRAKILL_Data/Managed/Stripped and a copy of Configgy in the libs folder.
