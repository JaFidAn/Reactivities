import { observer } from "mobx-react-lite";
import Calendar from "react-calendar";
import { Header, Menu, MenuItem } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

export default observer(function ActivityFilters() {
  const {
    activityStore: { predicate, setPredicate },
  } = useStore();
  return (
    <>
      <Menu vertical size="large" style={{ width: "100%", marginTop: 27 }}>
        <Header icon="filter" attached color="teal" content="Filters" />
        <MenuItem
          content="All Activities"
          active={predicate.has("all")}
          onClick={() => setPredicate("all", "true")}
        />
        <MenuItem
          content="I'm going"
          active={predicate.has("isGoing")}
          onClick={() => setPredicate("isGoing", "true")}
        />
        <MenuItem
          content="I'm hosting"
          active={predicate.has("isHost")}
          onClick={() => setPredicate("isHost", "true")}
        />
      </Menu>
      <Header />
      <Calendar
        onChange={(date) => setPredicate("startDate", date as Date)}
        value={predicate.get("startDate" || new Date())}
      />
    </>
  );
});
