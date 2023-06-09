Task:

In a typical school, students can move ahead with their learning, even if they didn’t fully understand or learn a topic. This is problematic for some highly “stacked” skills such as math because, for example, if one doesn’t fully understand fractions and exponents really well, they will struggle greatly doing more advanced algebra.

To address this issue and help students understand the importance of a solid foundation, we want to represent concepts students should know as blocks in a Jenga game. A national standardization organization called Common Core has already decided what individual concepts, aka standards, make up a grade level, see this example for 6th grade math.

In contrast to the traditional Jenga game, we do not expect the students to remove/put in blocks themselves - we shall do this for them based on the data we have collected from learning apps about their knowledge. However, we want to represent three distinct types of blocks:

    Glass, which symbolizes a concept the student does not know and needs to learn;
    Wood, which symbolizes a concept that the student has learned but was not tested on;
    Stone, which symbolizes a concept that the student was tested on and mastery was confirmed;

This structure helps the student better understand the topics they need to focus on, as it shows where they have gaps. Additionally, we want to enable gameplay modes that show how important it’s to have a strong stack when concepts build on top of one another. The game modes we’re envisioning right now can be briefly described:

    "Test my Stack" - eliminates all Glass blocks and activates the laws of physics, causing the stack to topple over if they have many gaps.
    "Strengthen my Stack" - test the student on a random Wood block in the stack to check if they really know it by assigning them a quiz.
    "Earthquake" - active physics and shake the ground to determine if the stack can withstand it.
    “Build my stack” - predicts how high their stack can be, when we start adding blocks from future grade levels on a shaky foundation. 
    “Challenge” - a multiplayer game that allows to challenge other students on pieces in their stack.

For this assessment, we want you to show your skills by developing the first game mode in Unity.
Requirements

Build a 3D Jenga game based on the above concepts and the following requirements:

    Use this API to fetch data about the Stacks (there are 3 stacks for 3 different grade levels)
    Put 3 stacks on a table in a row
        Use data from the API response to determine the block type:
            mastery = 0 → Glass
            mastery = 1 → Wood
            mastery = 2 →  Stone
        Put a label in front of each stack showing the grade associated with it (e.g., 8th Grade)
        Order the blocks in the stack starting from the bottom up, by domain name ascending, then by cluster name ascending, then by standard ID ascending
    Enable orbit controls on Mouse 1 hold so that the user can rotate the camera around the stack
        One of the 3 stacks should always be in focus by default
        Add some control to switch the view between the 3 stacks
    Add an option to get more details about a specific block (e.g., on right-click)
        Show the following details available in the API response
            [Grade level]: [Domain]
            [Cluster]
            [Standard Description]
    Implement the “Test my Stack” game mode described above, where you remove Glass blocks in the selected stack and enable physics


Explanations:
Time division:
 ~ 15min rereading and understaning problem
 ~ 10min Git/Project setup 
 ~ 4 hour development
 ~ 45 min ui modifications 
 ~ 30 min testing
 ~ 5 min git handling
 ~ 60 min video creation
 ~ 10 min video upload

Notes:

As mouse was mentioned I inferred that the game would be a PC game not mobile, visuals are based on a FullHD resolution, however should be scaleable.
First step is obtaining the data from the webpage provided
Added string modification for the JSON to simplify deserialization
Create stack blocks with futuere in mind (ex: mutliuple materials, mutliple block stacks)
Item with ID 114 has grade as "Algebra I" instead of a grade this causes problems with block creation
Input management can be improved
Movement is done simply
Popup management can be improved
Popups can be changed to 3D space
Input fairly messy can be simplified