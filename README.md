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
An important thing in this project, probably the most important is to measure the distance from the car to the walls around itself. As said before those green boxes are gonna be sensors, we have five so we can have more data and more flexibility around decisions of the direction.  
If we want to measure the distance to another object, in our case to the walls of the track, we simple go to our friend maths, yep maybe not so friendly but they work so put your head down and commit to them. We simply create a line, we pick the direction but we have to be a little careful cause the reference has to be each green box respectively. As we have our lines and direction then the job is done because walls can be treated as lines so the distance between two lines is easy to compute and we have the distance from each sensor to the closest wall.  
In our example this are the lines we are gonna cast to measure distance:
![image](https://user-images.githubusercontent.com/91338053/221320798-caccd78f-b8dc-4888-813a-98337bf81ae4.png)
So everything is going great, we have the track, the car and we can measure distances to the walls. Now we have to do an important thig, move the car.  
### Car Movement  
May sound strange or someting but this is easy, more whit Unity, just little lines of code and you can have a moving object. We are not gonna enter on details as how are collisions handle and hitoboxes and more cause is not what we need to focus but is not difficult.  
What we have to explain here is how our "Game" works. So the car is going to be moving forward in the direction its pointing always, can't go back just forward and then turn but as we turn we are going to rotate the car so we point it to another direction. This is important because when we cast the lines of before we have to have this in count because a sensor in the left not always have to cast the line to the same direction beacause it has rotate. Little bit tricky but sound way harder than it is.  
Another important thing about the movement is that the speed the car moves is set as the same at the start and never changes and for the rotation, because we want the car to rotate freely, a little or sometimes a lot, we meauser the rotation as a number between -1 up to 1 (-1 left, 1 right) and then we multiply that value with a factor of -0.22, this number is what controlls how "fast" rotate the car, the higher the faster so this number can be change for experiments what we proceed with this value cause is the perfect one for our example.  
Ok ok to much reading and words but, does it work? Check a Look:  
![upload](https://user-images.githubusercontent.com/91338053/221354285-956d2fff-17a7-430e-a014-3c192be1b2f9.gif)  
As you can see it moves the way we want and if you are curious yes the lines of the sensors are always facing the right way even if the car rotates. Now that everything works as we want we have to build the "game" itself.  
### Gameplay
Our "game" or better called simulation, is going to be as follow. We create one car each time and let them run as mention before until it dies and then spawn another one, we repeat the this process until we reach 20 cars. Once the 20 cars are done we pick the best two, mix the gens and generate another 20 and repeat until we want.  So using little bit of logic we can achive this, I am not going to dig in details but with this explanation you can have an idea how it goes and how to do it.  
### Closing Unity
Now that we have everything settle down in Unity is time to the spicy part. We can let Uniy and our UI on the sidelines and dive into how the car is going to make its own choices and how to create children from them. Yes I am talking about neural networks, lets create some life.  
## Brain of the Car
So we know that we have to give the car ability to make decisions on how much to rotate to avoid collisions, we are going to do that by a neural network.  
This neural net is going to have 5 input layers, one for each sensor, then some hidden layers that we can change and add more neurons or less to experiment and an ouput layer with only one neuron, this output is going to be wrap by a hyperbolic tangent function so we have an ouput in the range of -1 to 1.  
I know you want something visual so this is what our actual neural network looks like, you ask for this not me:

![image](https://user-images.githubusercontent.com/91338053/221355145-ff27ac66-247c-4018-85f4-40a43e3bb3d7.png)
It may seem like a chaos but trust me what you see is probably the simplest neural network architecture that exist, is something basic but does our job perfectly so thats what we want.    
Now that we have our net we have to define how we gonna create children. To do this we are going to mimic a little real genetics, we know that in everey layer there are a number of weights, thay create outputs and so on, so we are going to treat this weights as out genes and what we are gonna do is when we have our to best cars, each one with its own neural net, we take each layer of the net and split it at a random number so that the layer of children that we create is going to be a mix of the first part, up to the splitting point, of one and the second part of the other, from the splitting point to the last weight. And then be do this with all the layers we have.  
And finnaly but not less important we need some mutation, as in real life, so to achive this we simple create a probability of this to occur and if it happens we simply put a random value in the weight that has randomly be selected.  
Ok I know your brain is probably melt right now but is not that difficult, lets see an example so we can undertand it crearly:  
We have car A and car B and we only focus on one layer, imagine that in the first layer they have 10 weights an the split is going to be 5. So what we do is take the first 5 weights of the car A and the last 5 weights of car B, those weights go into the children car C so it now has 10 weights which 5 are from one parent and 5 from the other. Mutation is just take one random weight at change its value. See not that difficult.  

## Results
With everything implemented is time to run it and lets see how it goes. To visualize this what we are going to do is get some data of every generation so we can verify that its getting better a part from just looking at it which is the obvious part.  
So we run the experiment for 10 generations and lets see what was the best performance in each generation.  
 ![BestScoreEvolution](https://user-images.githubusercontent.com/91338053/222271689-1396f71c-1266-4590-a2c8-5eebcbfb1f1c.png)

As you can see each generation the cars are getting better and better, well at least one is improving so at first we can say its working cause it is, one small win so hurray!  
Also we can see as well that at the beginning we suffer a little lose cause the first generation its done randomly so the weights are random but as it goes mutating and breeding they become better.  

---

Now, we could firmly say that it works and we could stop here and continue our lifes with this tremdous win but lets go a little bit further. Maybe only one is better and the rest are not good enough so lets see how the whole generation is. To do this we simply plot the distribution of the scores of every single car in every generation and what be get is this.  
![car_evolution](https://user-images.githubusercontent.com/91338053/222271615-39580f12-8b5c-47bc-8670-b01745f10000.gif)

We can see that the further we go into generations they better they become cause more cars achive better scores which means that they survive longer. We can see as well that at some generation there are more cars that achive higher scores but if you pay attention you can see that the overal score of the population is better. So we get less top score individuals but more cars achive better score.  
So as unexpected as it may sound, it worked yeah. A new, fresh, good looking generation of cars has been borned.  
## Last Goodbye  
I feel sad but its time to say goodbye, we started this with a bunch of stupid cars. Now, they have achive knowledge and are capable of complete a full track without crashing. This is our final creation, enjoy and get out of here.
![final_car](https://user-images.githubusercontent.com/91338053/222275359-51cf103f-5efa-4215-99f9-be707e854e2c.gif)
