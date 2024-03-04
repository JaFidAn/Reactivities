import { observer } from "mobx-react-lite";
import {
  Divider,
  Grid,
  GridColumn,
  Header,
  Item,
  ItemContent,
  ItemGroup,
  ItemImage,
  Segment,
  Statistic,
  StatisticGroup,
} from "semantic-ui-react";
import { Profile } from "../../models/profile";
import FollowButton from "./FollowButton";

interface Props {
  profile: Profile;
}

export default observer(function ProfileHeader({ profile }: Props) {
  return (
    <Segment>
      <Grid>
        <GridColumn width={12}>
          <ItemGroup>
            <Item>
              <ItemImage
                avatar
                size="small"
                src={profile.image || "/assets/user.png"}
              />
              <ItemContent verticalAlign="middle">
                <Header as="h1" content={profile.displayName} />
              </ItemContent>
            </Item>
          </ItemGroup>
        </GridColumn>
        <GridColumn width={4}>
          <StatisticGroup widths={2}>
            <Statistic label="Followers" value={profile.followersCount} />
            <Statistic label="Following" value={profile.followingsCount} />
          </StatisticGroup>
          <Divider />
          <FollowButton profile={profile} />
        </GridColumn>
      </Grid>
    </Segment>
  );
});
