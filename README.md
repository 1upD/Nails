## Summary ##

Nails is intended to be a suite of tools for quickly blocking out maps on Valve's Source Engine. 

The first iteration of Nails is a random map generator that creates layouts and saves them in Source Engine VMF format. The concept of the generator was inspired by Mapgen and DungeonMaker.
http://www.gridsagegames.com/blog/2014/06/mapgen-tunneling-algorithm/ 
http://dungeonmaker.sourceforge.net/DM2_Manual/manual3.html

The generator works by simulating "artificial life forms" that carve out layouts by following randomly specified rules, ie tunnelers go straight each step with a certain percent chance of turning 90 degrees.

## How To Use ##

Usage of NailsCmd.exe:

  -f, --file        XML file containing agent configuration.

  -o, --output      Filename to write out to.

  -t, --lifetime    Number of steps in the simulation.

Writing an agent XML file:

The following XML demonstrates a random map configuration that has one tunneler and one roomer: It will generate a room and then a random path leading North from the room.

```xml
  <BaseAgent xsi:type="Tunneler">
    <X>0</X>
    <Y>0</Y>
    <Z>0</Z>
    <Width>3</Width>
    <Height>2</Height>
    <Lifetime>20</Lifetime>
    <MaxLifetime>20</MaxLifetime>
    <ProbReproduce>0.05</ProbReproduce>
    <ProbTurn>0.1</ProbTurn>
    <ProbAscend>0.05</ProbAscend>
    <Style>IndustrialTest</Style>
    <Direction>dev</Direction>
    <ProbSpawnRoomeer>0.005</ProbSpawnRoomeer>
    <MinHeightDecayRate>0</MinHeightDecayRate>
    <MaxHeightDecayRate>1</MaxHeightDecayRate>
    <MinWidthDecayRate>0</MinWidthDecayRate>
    <MaxWidthDecayRate>2</MaxWidthDecayRate>
    <SpawnRoomerOnDeath>true</SpawnRoomerOnDeath>
  </BaseAgent>

  <BaseAgent xsi:type="Roomer">
    <X>0</X>
    <Y>0</Y>
    <Z>0</Z>
    <Height>4</Height>
    <Style>IndustrialTest</Style>
  </BaseAgent>
```


## Dependencies ##

* VMFParser (https://github.com/BenVlodgi/VMFParser)
* CommandLineParser (https://github.com/commandlineparser/commandline)
* log4net (http://logging.apache.org/log4net/)
* Newtonsoft.Json (https://www.newtonsoft.com/json)