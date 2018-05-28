## Summary ##

Nails is intended to be a suite of tools for quickly blocking out maps on Valve's Source Engine. 

The first iteration of Nails is a random map generator that creates layouts and saves them in Source Engine VMF format. The concept of the generator was inspired by Mapgen and DungeonMaker.
http://www.gridsagegames.com/blog/2014/06/mapgen-tunneling-algorithm/ 
http://dungeonmaker.sourceforge.net/DM2_Manual/manual3.html

## How To Use ##

### Usage of NailsCmd.exe: ###

  -f, --file        XML file containing agent configuration.

  -o, --output      Filename to write out to.

  -t, --lifetime    Number of steps in the simulation.

### Writing an agent XML file: ### 

The generator works by simulating "artificial life forms" that carve out layouts by following randomly specified rules. The starting parameters for the artificial life simulation are configurable by an XML file, 'alife.xml'. The configuration can have any number of starting agents represented by the <BaseAgent/> tag in the XML.

Some agents have 'styles' which refer to sets of instances defined in the Nails folder. By default, there is one style called dev, located in Nails/dev.

Types of agents:
* Tunneler: Tunnelers 'dig', carving out empty tunnels or corridors by applying their given style. By default, they move in a straight line. For every cube the roomer marks as its style, there are set probabilities that it will reproduce a tunneler, produce a roomer, ascend or descend by one cube, or turn 90 degrees in either direction.
* Roomer: Roomers 'explode', creating rooms by marking large numbers of adjacent cubes as their given style in a rectangular pattern.

The following XML demonstrates a random map configuration that has one tunneler and one roomer: It will generate a room and then a random path leading North from the room.

```xml
  <!-- Tunnelers dig corridors or tunnels. -->
  <BaseAgent xsi:type="Tunneler">
	<!-- Starting X, Y, and Z coordinates for this Tunneler -->
	<X>0</X>
    <Y>0</Y>
    <Z>0</Z>
	<!-- Width of tunnel produced by this tunneler. The width is always perpendicular to the direction of motion. -->
    <Width>3</Width>
	<!-- Height of the tunnel produced by this tunneler. -->
    <Height>2</Height>
	<!-- Number of steps this tunneler will take before expiring -->
    <Lifetime>20</Lifetime>
	<!-- Maximum number of steps this tunneler can take before expiring.
	  -- Ideally, this number would be the same as the "Lifetime".
	  -- The difference is that MaxLifetime is propogated to children.
	  -- So if Lifetime is substantially longer or shorter than MaxLifetime,
	  -- that will apply to the first tunneler but not to any children
	  -- it produces. 
	  -->
    <MaxLifetime>20</MaxLifetime>
    <!-- Probability each step that this tunneler will create a new tunneler.
	  -- Should be a number between 0 and 1.
	  -- The new tunneler will move in a random direction 
	  -- perpendicular to this tunneler.
	  -->
	<ProbReproduce>0.05</ProbReproduce>
    <!-- Probability each step that this tunneler will turn 90 degrees.
	  -- Should be a number between 0 and 1.
	  -->
	<ProbTurn>0.1</ProbTurn>
    <!-- Probability each step that this tunneler will ascend or
	  -- descend by one cube.
	  -->
	<ProbAscend>0.05</ProbAscend>
    <!-- The style of instance this tunneler will apply.
	  -- The default is dev, a style provided with nails.
	  -->
	<Style>dev</Style>
    <!-- Direction this tunneler will start traveling in.
	  -->
	<Direction>East</Direction>
    <!-- Probability each step that this tunneler will create a roomer.
	  -- Should be a number between 0 and 1.
	  -- The roomer will deploy and create a room if most spaces in the
	  -- potential room have not been marked previously.
	  -->
    <ProbSpawnRoomeer>0.005</ProbSpawnRoomeer>
	<!-- Minimum amount to subtract from height when producing children.
	  -- No height will be subtracted if the height of the parent is 1.
	  -->
    <MinHeightDecayRate>0</MinHeightDecayRate>
    <!-- Maximum amount to subtract from height when producing children.
	  -- No height will be subtracted if the height of the parent is 1.
	  -->
	<MaxHeightDecayRate>1</MaxHeightDecayRate>
    <!-- Minimum amount to subtract from width when producing children.
	  -- No width will be subtracted if the width of the parent is 1.
	  -->
	<MinWidthDecayRate>0</MinWidthDecayRate>
    <!-- Maximum amount to subtract to height when producing children.
	  -- No height will be subtracted if the height of the parent is 1.
	  -->
	<MaxWidthDecayRate>2</MaxWidthDecayRate>
    <!-- Should this tunneler leave a roomer behind when it expires? -->
	<SpawnRoomerOnDeath>true</SpawnRoomerOnDeath>
  </BaseAgent>

  <!-- Roomers produce rooms. -->
  <BaseAgent xsi:type="Roomer">
  	<!-- Starting X, Y, and Z coordinates for this roomer. -->
    <X>0</X>
    <Y>0</Y>
    <Z>0</Z>
	<!-- Height of the room produced by this roomer. -->
    <Height>4</Height>
    <!-- The style of instance this roomer will apply.
	  -- The default is dev, a style provided with nails.
	  -->
    <Style>dev</Style>
  </BaseAgent>
```


## Dependencies ##

* VMFParser (https://github.com/BenVlodgi/VMFParser)
* CommandLineParser (https://github.com/commandlineparser/commandline)
* log4net (http://logging.apache.org/log4net/)
* Newtonsoft.Json (https://www.newtonsoft.com/json)