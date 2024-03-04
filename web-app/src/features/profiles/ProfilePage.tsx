import { observer } from "mobx-react-lite";
import ProfileHeader from "./ProfileHeader";
import { Grid, GridColumn } from "semantic-ui-react";
import ProfileContent from "./ProfileContent";
import { useStore } from "../../app/stores/store";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default observer(function ProfilePage() {
  const { profileStore } = useStore();
  const { username } = useParams<{ username: string }>();
  const { loadingProfile, loadProfile, profile, setActiveTab } = profileStore;

  useEffect(() => {
    if (username) loadProfile(username);
    return () => {
      setActiveTab(0);
    };
  }, [loadProfile, setActiveTab, username]);

  if (loadingProfile) return <LoadingComponent content="Loading profile..." />;
  return (
    <Grid>
      <GridColumn width={16}>
        {profile && (
          <>
            <ProfileHeader profile={profile} />
            <ProfileContent profile={profile} />
          </>
        )}
      </GridColumn>
    </Grid>
  );
});
