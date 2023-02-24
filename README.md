# Genetic_Cars
## Not Funny Introduction
So... Cars and genetics, what can go wrong right? Trying to be God? Yep, simple as that. Ok simple summary, we take an initial sample of cars then we let them go and live their life, when they crash they die, we pick the two best ones (the two cars that last longer) and let them become parents of another 20 newborn little cars with hight expectations and dreams for life and we simply repeat the process everytime until they become better and better and we finnaly reach the perfect population with no problems so they can rule the WORLD (Evil laugh here)   

## Now The Step by Step Explanation  
Ok so now we know what we are triying to do. Now the difficult part, do it.  
Lets start for the simple things and step by step, we first need a UI, we need to have the car and a track to let the car live. How we do that? Lets go to Unity, simple as that.  
### User Interface
Now that we are game developers, we have to build a car and a track. Lets start with the track because the car has little details we will mention later.  
#### Track
So the track is simply walls, 2D walls so we can create a track and have hitboxes to detect collision. The result is something like this:
![image](https://user-images.githubusercontent.com/91338053/221318713-60698cf4-5444-4f19-a611-625ce3e1568d.png)  
Don't led attention into the car, it's a spoiler. The track is incredible and if you have another opinion I don't care is my creation, my Mona Lisa shut up.  
Ok so we have a track, nice, now we need a car to move cause is important so lets gooooo!!! What am I doing with my life...  
#### Car  
Out car is gonna be a simple red rectangle but we need to measure the distance to the walls from the car in multiple directions so the car have enought data to make decisions on were to move and don't crash. To create this sensors we are going to create five green boxes and place them around the car, is more a visual thing for the aesthetic but as well for utility cause those boxes are gonna be are initial position for the lines to measure the distance, more of this later on. Ok so the car will look something like this:  
![image](https://user-images.githubusercontent.com/91338053/221319750-580bfbc2-052b-46cd-88bd-c1914c84be14.png)
The same as before, shut up about the look, I know it looks like a spider from Minecraft but is ok I'm not going to model something different.  
### Line Cast (RayCast)
An important thing in this project, probably the most important is to measure the distance from the car to the walls around itself. As said before those green boxes are gonna be are sensors, we have five so we can have more data and more flexibility around decisions of the direction.  
If we want to measure the distance to another object, in our case to the walls of the track, we simple go to our friend maths, yep maybe not so friendly but they work so put our head down and commit to them. We simply create a line, we pick the direction but we have to be a little careful cause the reference has to be each green box respectively. As we have our lines and direction then the job is done because walls can be treat as walls so the distance between to lines is easy to compute and we have the distance from each sensor to the closest wall.  
In our example this are the lines we are gonna cast to measure distance:
![image](https://user-images.githubusercontent.com/91338053/221320798-caccd78f-b8dc-4888-813a-98337bf81ae4.png)
