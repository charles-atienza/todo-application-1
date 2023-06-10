----How to run----
Prerequisites that I used: .NET Core 6, NodeJS, SQL Server 2022, Visual Studio 2022, Microsoft SQL Server Management Studio 18

1. Open backend -> Exam.sln
2. Right click on Exam.Core then click on the manage user secret.
3. Input your connection string in the user secret. sample below:

{
    "ConnectionStrings": {
        "Default": "Server=localhost\\MyServer;Database=DatabaseToUse;User Id=UserForSysAdmin;Password=PasswordWithSysAdminAccess;MultipleActiveResultSets=true; TrustServerCertificate=True;"
    }
}

4. once opened, open Package Manager Console.
5. in Package Manager Console, change the default project selected to Exam.EntityFrameworkCore
6. then run the command "update-database" to create the database.

7. once all of the above are successful
8. open a command prompt and navigate to the frontend folder
9. then enter "npm install" to install all the dependencies
10. then enter "npm start" to start the frontend

----Assumed----
1. There are 3 buttons 
    * Left Button: rotate 90° anticlockwise/counterclockwise.
        - If Current Direction is North then rotate to West
    * Right Button: rotate 90° clockwise.
        - If Current Direction is North then rotate to East
    * Move Button: move 1 step forward.
        - If Current Direction is North then move 1 step forward in North direction
2. There is a 5x5 grid. !!!!NOTICE!!!! as the user can create a table on their own, the grid 5x5 won't exist unless the user creates it.
    * Rules is that Move Button should only work if the robot is inside the grid after moving 1 step forward.
3. For Every Action there is a report of the current position and direction of the robot.
    * Additional information is that the robot is not allowed to move outside the grid if the next move is outside the grid.
4. The robot is initially placed at (0,0) and facing East. This is also true when the user changes the table, it will reset the robots position.
    * 0,0 is top left corner which means that south > north.
        - Eg: grid is 5x5; then 0,0 is top left corner and 4,4 is bottom right corner.
    * Going south is 0,1 and going north is 0,-1.
5. The index for each grid starts at 0,0. Which means 5 x 5 grid has index 0 to 4 on X and Y axis.
6. I assume that the UI or the frontend is not part of the task and the architecture and codes will not be rated.
7. I assume that there are no degrees of direction to return as the direction returned is in the four cardinal directions: NORTH, EAST, SOUTH or WEST.
8. This part is for the added functionality that isn't included on the task. 

----Comments----
I assume that it is alright to add more functionality to make it easier to navigate the frontend and to show how good the architecture is.
I have added many common functions that would mostlikely always exists in a web api project.