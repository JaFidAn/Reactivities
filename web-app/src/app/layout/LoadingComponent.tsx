import { Dimmer, Loader } from "semantic-ui-react";

type Props = {
  inverted?: boolean;
  content?: string;
};

export default function LoadingComponent({
  inverted = true,
  content = "Loading...",
}: Props) {
  return (
    <Dimmer active={true} inverted={inverted}>
      <Loader content={content} />
    </Dimmer>
  );
}
