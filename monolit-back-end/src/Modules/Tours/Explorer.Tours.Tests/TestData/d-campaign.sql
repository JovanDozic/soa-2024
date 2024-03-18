INSERT INTO tours."Campaign"("Id", "JsonObject")
VALUES (-1, '{{
    "Id": 1,
    "Title": "Title",
    "Tours":[{
        "id": 1,
        "name": "Excursion to Spain",
        "description": "A fantastic journey through the landscapes of Spain",
        "difficult": 3,
        "status": 1,
        "price": 250,
        "points": [
          {
            "name": "Madrid Central Plaza",
            "description": "A bustling city center with historical significance",
            "latitude": 40.416775,
            "longitude": -3.70379,
            "picture": "madrid_plaza.jpg",
            "public": true
          }
        ],
        "tags": [
          {
            "name": "Adventure"
          },
          {
            "name": "Historical"
          }
        ],
        "requiredTimes": [
          {
            "transportType": 1,
            "minutes": 45
          }
        ],
        "reviews": [
          {
            "rating": 5,
            "comment": "An unforgettable experience, highly recommended!",
            "touristId": 101,
            "touristUsername": "traveler_jane",
            "tourDate": "2023-11-15T10:00:00.000Z",
            "creationDate": "2023-11-20T09:00:00.000Z",
            "images": [
              "review1.jpg"
            ]
          }
        ],
        "authorId": 34,
        "length": 150,
        "publishTime": "2023-10-01T08:00:00.000Z",
        "arhiveTime": "2024-10-01T08:00:00.000Z",
        "problems": [
          {
            "id": 1,
            "category": "Logistics",
            "priority": false,
            "description": "Delay in transportation schedule",
            "time": "2023-11-12T07:30:00.000Z",
            "tourId": 1,
            "touristId": 205,
            "authorsSolution": "Provided alternate transportation",
            "isSolved": true,
            "unsolvedProblemComment": "",
            "deadline": "2023-11-13T20:00:00.000Z"
          }
        ]
      },
      {
        "id": 2,
        "name": "Alpine Adventure",
        "tags": [
          {
            "name": "Nature"
          },
          {
            "name": "Hiking"
          }
        ],
        "price": 350,
        "length": 200,
        "points": [
          {
            "name": "Eagles Nest",
            "public": true,
            "picture": "eagles_nest.jpg",
            "latitude": 47.516231,
            "longitude": 14.550072,
            "description": "Scenic viewpoint atop the Alpine ridge"
          },
          {
            "name": "Alpine Lake",
            "public": false,
            "picture": "alpine_lake.jpg",
            "latitude": 47.269212,
            "longitude": 11.404102,
            "description": "Crystal clear waters surrounded by mountains"
          }
        ],
        "status": 2,
        "reviews": [
          {
            "images": [
              "review_alpine.jpg"
            ],
            "rating": 4,
            "comment": "Stunning views, but quite challenging. Be prepared!",
            "tourDate": "2023-08-05T12:00:00.000Z",
            "touristId": 202,
            "creationDate": "2023-08-10T15:30:00.000Z",
            "touristUsername": "mountain_lover"
          }
        ],
        "authorId": 56,
        "problems": [
          {
            "id": 2,
            "time": "2023-08-02T16:45:00.000Z",
            "tourId": 2,
            "category": "Weather",
            "deadline": "2023-08-04T18:00:00.000Z",
            "isSolved": true,
            "priority": true,
            "touristId": 303,
            "description": "Unexpected snowstorm",
            "authorsSolution": "Rerouted to safer paths, provided extra gear",
            "unsolvedProblemComment": ""
          }
        ],
        "difficult": 5,
        "arhiveTime": "2024-06-01T09:30:00.000Z",
        "description": "Breathtaking hike through the Alpine trails",
        "publishTime": "2023-06-01T09:30:00.000Z",
        "requiredTimes": [
          {
            "minutes": 60,
            "transportType": 2
          }
        ]
      }
    ],
    "TouristId": 1,
    "Difficult": 3,
    "Length": 5.8
}}')