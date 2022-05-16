import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
import { Header, Icon, List, Image } from 'semantic-ui-react';


function App() {
  const [activities, setActivities] = useState([]);

  useEffect(()=>{
    axios.get("http://localhost:5046/api/activities").then(response=>{
      console.log(response);
      setActivities(response.data);
    })
  }, [])

  return (
    <div className="App">
      
      <Header as='h2' icon textAlign='center'>
      <Icon name='users' circular />
      <Header.Content>Reactivities</Header.Content>
    </Header>
    <Image
      centered
      size='large'
      src='client-app\src\images\zer.png'
    />
    
     <List>

     {activities.map((activity: any) => (
           <List.Item key={activity.id}>
             {activity.title}
           </List.Item>
         ))}

     </List>
        

    </div>
  );
}

export default App;
