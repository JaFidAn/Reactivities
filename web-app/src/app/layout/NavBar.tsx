import {
  Button,
  Container,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  Image,
  Menu,
  MenuItem,
} from "semantic-ui-react";
import { Link, NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";

export default observer(function NavBar() {
  const {
    userStore: { user, logout },
  } = useStore();
  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item as={NavLink} to="/" header>
          <img src="/assets/logo.png" alt="logo" style={{ marginRight: 10 }} />
          Reactivities
        </Menu.Item>
        <Menu.Item as={NavLink} to="/activities">
          Activities
        </Menu.Item>
        <Menu.Item as={NavLink} to="/errors">
          Errors
        </Menu.Item>
        <Menu.Item as={NavLink} to="/createActivity">
          <Button positive content="Create Activity" />
        </Menu.Item>
        <MenuItem position="right">
          <Image
            src={user?.image || "/assets/user.png"}
            avatar
            spaced="right"
          />
          <Dropdown pointing="top left" text={user?.displayName}>
            <DropdownMenu>
              <DropdownItem
                as={Link}
                to={`/profiles/${user?.username}`}
                text="My Profile"
                icon="user"
              />
              <DropdownItem onClick={logout} text="Logout" icon="power" />
            </DropdownMenu>
          </Dropdown>
        </MenuItem>
      </Container>
    </Menu>
  );
});
