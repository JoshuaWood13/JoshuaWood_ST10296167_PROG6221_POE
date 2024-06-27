Name: Joshua Wood
Student number: ST10296167
Group: 2

PROG POE PART 3

GitHub repository link: 
https://github.com/JoshuaWood13/JoshuaWood_ST10296167_PROG6221_POE.git

# How to compile and run the software:

*Make sure you have the Microsoft Visual Studio IDE installed!* 

1. Right click on the project zip file and select extract all and then choose a location to extract to.

2. Open Microsoft Visual Studio and under "get started" select "Open a project or solution".

3. Navigate to find the location where you stored the extracted project zip file.

4. Inside the project folder you will find a solution file titled: "JoshuaWood_ST10296167_PROG6221_POE.sln". Run this solution file.

5. The project should now open inside of Visual Studio. In order to compile and run the software for Part 3, you need to make sure that you have WpfApp_Part3_POE selected as the startup project.
   This should be done automatically but in case its not, go to the navigation bar located at the top of the IDE. There you will find a green play button titled "Start". To the left of this button you will find a dropdown menu
   that allows you to select the project to run. Make sure you set this to WpfApp_Part3_POE. Now all you have to do is click on the play button and the software will compile and run, opening the WPF application.

6. The application can be exited by user input by clicking the close button located at the top right of the current window, but in case of an error, it can also be exited by pressing the red "Stop debugging" icon in the IDE navigation bar.


# How to use the application:

A more detailed user manual document has been created in order to assist you in using the Recipe Generator application. 
Make sure to refer to the user manual document if you have any questions on how to use the application.

1) Create a recipe
2) Display recipe
3) Scale recipe
4) Reset recipe
5) Clear recipe
6) Quit

Enter choice:

*Follow the on-screen menu prompts to interact with the application*

• Type 1 to create a new recipe. You will be prompted to enter all ingredient details and steps.

• Type 2 to choose what recipe you would like to diplay. It will display the recipe name, calories, ingredients and steps.

• Type 3 to choose what recipe you would like to scale. You will be given 3 scaling options to choose from.

• Type 4 to choose a recipe to reset. This will reset the recipes ingredients and calorie values back to the original values.

• Type 5 to choose a recipe to clear. This will completely remove the recipe from memory. You will be prompted to confirm this action.

• Type 6 to exit the application. Recipe data will not be saved 


# Updates based on lecturer feedback

Due to getting 100% for Part 2 of the POE I did not receive any lecturer feedback pertaining to making any changes or updates to my code. 

Therefore I made sure to maintain the same standard for Part 3. Ensuring that all functionality found in Part 2 is present in Part 3. 
Along with the additional functionality of filtering the list of recipes. I made sure to maintain good code structure and included all the necessary comments to explain logic.

There were two elements of my Part 1 that needed to be slightly improved on for Part 2: my error handling and my README file.

1. For improving my error handling I needed to add extra functionality in order to handle negative number inputs and zero inputs.
To do this I added an additional condition to the while loop for my validNum and validDouble methods. This condition checks if the entered input is below 
a certain range. Thus ensuring the entered int or double is not a negative or not a zero. Additionally, I also added a new validString method that ensures 
user entered strings for the recipe name, ingredient names and steps are not null and does not contain any numbers. This helps to prevent unintentional
user error whilst also creating a more consistent experience.

2. For improving my README file I added an additional section to my README file that went through how to actually navigate and use the application as can 
be seen under the "How to use the appplication" heading. I did this by providing a sample of the menu output structure and then providing a detailed description
of how to choose each menu option and what each option will do.

